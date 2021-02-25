<template>
  <v-list-group
    v-if="menuItem.children"
    :disabled="menuItem.isDisabled"
    :prepend-icon="isRoot ? menuItem.fontIcon : undefined"
    :class="'navigation-menu-list-group ' + (wrapperIsMinified ? 'wrapper-minified' : 'wrapper-collapsed')"
    :sub-group="!isRoot"
    :title="wrapperIsMinified ? menuItem.name : ''"
  >
    <template #activator>
      <v-list-item-title v-text="menuItem.name" />
    </template>
    <NavigationMenuItem
      v-for="(child, index) in menuItem.children"
      :key="menuItem.name + ' - ' + child.name + ' - ' + index"
      :menu-item="child"
      :wrapper-is-minified="wrapperIsMinified"
    />
  </v-list-group>
  <v-list-item
    v-else
    :key="menuItem.title"
    :disabled="menuItem.isDisabled"
    dense
    :title="wrapperIsMinified ? menuItem.name : ''"
    @click="onItemCLickHandle(menuItem.to)"
  >
    <v-list-item-icon class="mr-4">
      <v-icon>{{ menuItem.fontIcon }}</v-icon>
    </v-list-item-icon>
    <v-list-item-content>
      <v-list-item-title v-text="menuItem.name" />
    </v-list-item-content>
  </v-list-item>
</template>
<script>
export default {
  name: 'NavigationMenuItem',
  props: {
    menuItem: { type: Object, default: null },
    isRoot: Boolean,
    wrapperIsMinified: Boolean,
  },
  methods: {
    onItemCLickHandle(to) {
      const path = to;
      if (this.$route.path !== path) this.$router.push(path);
    },
  },
};
</script>
<style>
.navigation-menu-list-group.wrapper-collapsed .v-list-group__items,
.navigation-menu-list-group .v-list-group__header {
  padding-left: 16px !important;
}
.navigation-menu-list-group .v-list-item__icon:first-child {
  margin-right: 16px;
}
</style>
