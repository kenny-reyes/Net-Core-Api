<template>
  <v-container fluid class="pa-0 pa-lg-5">
    <v-row class="ml-3 mt-3 mt-lg-0">
      <h2 class="text--secondary">Settings</h2>
    </v-row>

    <v-row>
      <v-col>
        <v-expansion-panels inset>
          <v-expansion-panel>
            <v-expansion-panel-header>Appearence</v-expansion-panel-header>
            <v-expansion-panel-content>
              <v-container fluid>
                <v-switch v-model="$vuetify.theme.dark" :label="`Dark theme: ${$vuetify.theme.dark.toString()}`" />
              </v-container>
            </v-expansion-panel-content>
          </v-expansion-panel>
          <v-expansion-panel disabled>
            <v-expansion-panel-header>Colors</v-expansion-panel-header>
            <v-expansion-panel-content />
          </v-expansion-panel>
        </v-expansion-panels>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: 'SettingsView',
  components: {},
  data: () => ({}),
  methods: {},
};
</script>

<style lang="scss" scoped>
</style>
