module.exports = {
  transpileDependencies: [
    'vuetify'
    ],
    devServer: {
        // https://medium.com/js-dojo/how-to-deal-with-cors-error-on-vue-cli-3-d78c024ce8d3
        // This will tell the dev server to proxy any unknown requests(requests that did not match a static file) to http://backend.test/
        proxy: 'http://localhost'
    }
}
