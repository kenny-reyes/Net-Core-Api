import * as msal from '@azure/msal-browser';
import axios from 'axios';
import { InteractionRequiredAuthError, LogLevel } from '@azure/msal-common';

export default class AuthService {
  constructor(router) {
    this.tokenResponse = null;
    this.config = {
      auth: {
        clientId: process.env.VUE_APP_MSAL_CLIENTID,
        authority: process.env.VUE_APP_MSAL_AUTHORITY,
        validateAuthority: false,
        loginRedirect: process.env.VUE_APP_MSAL_REDIRECT,
        postLogoutRedirectUri: process.env.VUE_APP_MSAL_REDIRECT,
        navigateToLoginRequestUrl: false,
      },
      cache: {
        cacheLocation: 'sessionStorage',
        storeAuthStateInCookie: false,
      },
      system: {
        loggerOptions: {
          loggerCallback: this.logCallback,
          piiLoggingEnabled: false,
        },
        windowHashTimeout: 60000,
        iframeHashTimeout: 6000,
        loadFrameTimeout: 0,
        asyncPopups: false,
      },
    };
    this.loginRequest = {
      scopes: ['User.Read'],
    };
    this.msalInstance = new msal.PublicClientApplication(this.config);
    this.msalInstance.handleRedirectPromise().catch((error) => {
      this.logCallback(LogLevel.Error, error, false);
    });

    this.registerAxiosInterceptor();
    this.routerAuthConfig(router);
  }

  logCallback(level, message, containsPii) {
    if (containsPii) {
      return;
    }
    switch (level) {
      case LogLevel.Error:
        console.error(message);
        return;
      case LogLevel.Info:
        console.info(message);
        return;
      case LogLevel.Verbose:
        console.debug(message);
        return;
      case LogLevel.Warning:
        console.warn(message);
    }
  }

  routerAuthConfig(router) {
    router.beforeEach((to, from, next) => {
      if (to.matched.some((record) => record.meta.requiresAuth)) {
        if (!this.isAuthenticated()) {
          this.signIn();
        } else {
          next();
        }
      } else {
        next();
      }
    });
  }

  registerAxiosInterceptor() {
    axios.interceptors.request.use(
      async (config) => {
        const session = await this.getToken();
        if (session.idToken) {
          config.headers = {
            Authorization: `Bearer ${session.idToken}`,
            'Content-Type': 'application/json',
          };
          return config;
        }
        return config;
      },
      (err) => Promise.reject(err)
    );
  }

  async getToken() {
    const currentAccount = this.getUserAccount();
    if (!currentAccount) {
      await this.signIn();
    } else {
      const silentRequest = {
        scopes: this.loginRequest.scopes,
        account: currentAccount,
        forceRefresh: false,
      };
      const request = {
        scopes: this.loginRequest.scopes,
        loginHint: currentAccount.username,
      };

      return this.msalInstance.acquireTokenSilent(silentRequest).catch((error) => {
        if (error instanceof InteractionRequiredAuthError) {
          return this.msalInstance.acquireTokenRedirect(request);
        }
      });
    }
  }

  async signIn() {
    return await this.msalInstance.loginRedirect(this.loginRequest).catch((error) => {
      this.logCallback(LogLevel.Error, error, false);
    });
  }

  async signOut() {
    const currentAccount = this.getUserAccount();
    await this.msalInstance
      .logout({
        account: currentAccount,
      })
      .catch((reason) => {
        this.logCallback(LogLevel.Error, reason, false);
      });
  }

  getUserAccount() {
    return this.msalInstance.getAllAccounts()[0];
  }

  isAuthenticated() {
    const accounts = this.msalInstance.getAllAccounts();
    return accounts.length > 0;
  }
}
