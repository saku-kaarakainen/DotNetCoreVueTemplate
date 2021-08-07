<!--
  2021-08-07: Template modified from https://github.com/SoftwareAteliers/asp-net-core-vue-starter/blob/master/ClientApp/src/views/FetchData-decorator.vue
-->
<template>
  <v-container fluid>
    <!--
  <v-slide-y-transition mode="out-in">
    <v-row>
      <v-col>
        <h1>Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <v-data-table :headers="headers"
                      :items="forecasts"
                      hide-default-footer
                      :loading="loading"
                      class="elevation-1">
          <template v-slot:progress>
            <v-progress-linear color="blue" indeterminate></v-progress-linear>
          </template>
          <template v-slot:[`item.date`]="{ item }">
            <td>{{ item.date | date }}</td>
          </template>
          <template v-slot:[`item.temperatureC`]="{ item }">
            <v-chip :color="getColor(item.temperatureC)" dark>{{ item.temperatureC }}</v-chip>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
  </v-slide-y-transition>

  <v-alert :value="showError" type="error" v-text="errorMessage">
    This is an error alert.
  </v-alert>

  <v-alert :value="showError" type="warning">
    Are you sure you're using ASP.NET Core endpoint? (default at
    <a href="http://localhost:5000/fetch-data">http://localhost:5000</a>)<br />
    API call would fail with status code 404 when calling from Vue app (default at
    <a href="http://localhost:8080/fetch-data">http://localhost:8080</a>) without devServer proxy
    settings in vue.config.js file.
  </v-alert>
  -->
  </v-container>
</template>

<!-- This is no longer work of SoftwareAteliers. -->
<script lang="ts">
import { computed } from 'vue'
export default {
  // props � contains all of the props passed to our component
  // context � contains attrs, slots, and emit
  setup (props:any, context:any): void {
    console.log('hello world')
  }
}
</script>

<style lang="scss" scoped>
</style>
