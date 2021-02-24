<template>
  <div class="pa-2">
    <v-dialog v-model="dialog" max-width="290">
      <v-card light>
        <v-card-title> Logout </v-card-title>
        <v-card-text> Are you sure you want to logout? </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text color="accent" @click="dialog = false" v-on:click="logout()"> Logout </v-btn>
          <v-btn text @click="dialog = false"> Cancel </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>
<script>

export default {
  name: 'UserManager',
  components: {
  },
  data() {
    return {
      dialog: false,
      refreshData: { type: Boolean, default: false },
      listKey: 0,
    };
  },
  methods: {
  },
};
</script>
