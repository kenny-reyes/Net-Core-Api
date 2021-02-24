<template>
  <v-list dense>
    <v-list-item-group color="primary" mandatory>
      <NavigationMenuItem
        v-for="(menuItem, index) in menuItems"
        :key="'base' + index"
        :menuItem="menuItem"
        :isRoot="true"
        :wrapperIsMinified="wrapperIsMinified"
      />
    </v-list-item-group>
  </v-list>
</template>

<script>
import NavigationMenuItem from '@/components/mainLayout/NavigationMenuItem';

export default {
  name: 'NavigationDrawerMenu',
  components: {
    NavigationMenuItem,
  },
  props: {
    wrapperIsMinified: Boolean,
    menuItems: {
      type: Array,
      default() {
        return [];
      },
    },
  },
};
</script>
