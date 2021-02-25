<template>
  <div class="d-flex justify-center align-center">
    <v-menu transition="slide-y-transition" bottom offset-y>
      <template #activator="{ on, attrs }">
        <v-btn large color="primary" v-bind="attrs" elevation="3" rounded fav block v-on="on">
          <strong> {{ account.name }} </strong>
          <v-divider class="mx-2" vertical />
          <v-icon large right> mdi-account-circle </v-icon>
        </v-btn>
      </template>

      <v-card light width="250px">
        <v-list>
          <v-list-item>
            <v-list-item-avatar large>
              <v-avatar color="primary">
                <span class="white--text headline">{{ initials }}</span>
              </v-avatar>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title>{{ account.name }}</v-list-item-title>
              <v-list-item-subtitle>{{ account.username }}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>
        <v-divider />
        <v-list dense>
          <v-list-item-group color="secondary">
            <v-list-item v-for="(item, i) in items" :key="i">
              <v-list-item-icon>
                <v-icon v-text="item.icon" />
              </v-list-item-icon>
              <v-list-item-content>
                <v-list-item-title v-text="item.text" />
              </v-list-item-content>
            </v-list-item>
          </v-list-item-group>
        </v-list>
        <v-card-actions>
          <v-btn block elevation="2" color="accent" @click.stop="dialog = true"> Logout </v-btn>
        </v-card-actions>
      </v-card>
    </v-menu>

    <v-dialog v-model="dialog" max-width="350">
      <v-card light>
        <v-card-title> {{ logoutDialogTittle }} </v-card-title>
        <v-card-text> {{ logoutDialogMessage }} </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn text color="accent" @click="dialog = false" @click.stop="logout()"> {{ logoutButtonAccept }} </v-btn>
          <v-btn text @click="dialog = false"> {{ logoutButtonCancel }} </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: 'ProfileMenu',

  data() {
    return {
      account: {
        name: 'Kenny Reyes',
        userName: 'kenny@mail.com',
      },
      logoutDialogTittle: 'Logout',
      logoutDialogMessage: 'Are you sure you want to logout?',
      logoutButtonAccept: 'Logout',
      logoutButtonCancel: 'Cancel',
      dialog: false,
      selectedItem: 0,
      items: [
        { text: 'Profile', icon: 'mdi-folder' },
        { text: 'Updates', icon: 'mdi-folder' },
        { text: 'Messages', icon: 'mdi-account-multiple' },
        { text: 'Tasks', icon: 'mdi-star' },
        { text: 'Comments', icon: 'mdi-history' },
        { text: 'Projects', icon: 'mdi-check-circle' },
        { text: 'Payments', icon: 'mdi-upload' },
        { text: 'Settings', icon: 'mdi-cloud-upload' },
      ],
    };
  },
  computed: {
    initials() {
      const initials = this.account.name.match(/\b\w/g) || [];
      return ((initials.shift() || '') + (initials.pop() || '')).toUpperCase();
    },
  },
  methods: {
    logout() {
      alert('Execute logout');
    },
  },
};
</script>
