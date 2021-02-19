<template>
  <v-app>
    <v-footer color="footer" app elevation="8">
      <div class="ml-auto">
        <span class="mr-1">Ubicua Remote Monitoring by</span>
        <a href="https://zertia.es" target="_blank">Zertia</a>
      </div>
      <span>&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>

    <v-app-bar color="appBar" app clipped-left>
      <v-app-bar-nav-icon @click.stop="mini = !mini" />

      <v-btn target="_blank" text @click="$router.push({ path: '/' }).catch((err) => {})">
        <div class="d-flex align-items-center">
          <v-row>
            <v-col class="pa-0">
              <v-img
                :alt="applicationTitle"
                :src="applicationIcon"
                transition="scale-transition"
                height="65px"
              />
            </v-col>
            <v-col class="align-self-center pa-1">
              <v-toolbar-title>{{ applicationTitle }}</v-toolbar-title>
            </v-col>
          </v-row>
        </div>
      </v-btn>

      <v-spacer />

      <ProfileMenu />
    </v-app-bar>
    <v-navigation-drawer
      color="navigationPanel"
      v-model="drawer"
      app
      :mini-variant="mini"
      clipped
      permanent
    >
      <NavigationMenu :menuItems="menuItems" :wrapperIsMinified="mini" />
    </v-navigation-drawer>
    <v-main class="main-content">
      <transition name="fade">
        <router-view />
      </transition>
    </v-main>
  </v-app>
</template>

<script>
import ProfileMenu from '@/components/mainLayout/ProfileMenu';
import NavigationMenu from '@/components/mainLayout/NavigationDrawerMenu';

export default {
  name: 'MainLayout',
  components: {
    ProfileMenu,
    NavigationMenu,
  },
  data() {
    return {
      applicationTitle: this.$rmConfig.appTitle,
      applicationIcon: this.$rmConfig.darkTheme
        ? this.$rmConfig.darkLogoIcon
        : this.$rmConfig.lightLogoIcon,
      drawer: null,
      mini: true,
      valor: false,
      menuItems: this.$rmConfig.menuItems.concat(this.$rmConfig.embeddedSections),
    };
  },
};
</script>
<style>
html {
  overflow-y: hidden;
}
.main-content {
  overflow-y: scroll;
  height: 100%;
  width: 100%;
  position: absolute;
}
.v-footer {
  z-index: 5;
}
</style>
