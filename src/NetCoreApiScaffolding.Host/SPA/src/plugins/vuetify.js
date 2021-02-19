import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';

Vue.use(Vuetify);

export default function GetVuetify(configuration) {
  const vuetify = new Vuetify({
    icons: {
      iconfont: 'mdi',
    },
    theme: {
      dark: configuration.darkTheme,
      themes: configuration.themes,
    },
  });
  return vuetify;
}
