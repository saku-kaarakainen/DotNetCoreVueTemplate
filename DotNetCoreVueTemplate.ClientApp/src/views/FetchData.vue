<template>
  <!--
    https://stackoverflow.com/a/64039546
    You need to use Suspense tag, when using async setup
    -->
  <Suspense>
    <!-- the normal case -->
    <template #default>
      <table>
        <tr>
          <th v-for="header in headers" :key="header.value">{{ header.text }}</th>
        </tr>
        <tr v-for="forecast in forecasts" :key="forecast">
          <td>{{ forecast.date }}</td>
          <td :style="{ color: getColor(forecast.temperatureC) }">{{ forecast.temperatureC }}</td>
          <td :style="{ color: getColor(forecast.temperatureF) }">{{ forecast.temperatureF }}</td>
          <td>{{ forecast.summary }}</td>
        </tr>
      </table>
    </template>

    <!-- the asynchronous call is not done yet. -->
    <template #fallback>
      <span>Loading...</span>
    </template>
  </Suspense>
</template>

<script lang="ts">
  import { Forecast } from '../models/forecast'
  import { ApiService } from '../services/ApiService'

  export default {
    async setup() {
      const service = new ApiService()

      // Initializing properties
      var showError = false
      var errorMessage = 'Error while loading weather forecast'
      var headers = [
        { text: 'Date', value: 'date' },
        { text: 'Temp. (C)', value: 'temperatureC' },
        { text: 'Temp. (F)', value: 'temperatureF' },
        { text: 'Summary', value: 'summary' }
      ]
      var forecasts = Array<Forecast>()

      // Declare functions
      function getColor(temperature: number): string {
        if (temperature < 0) {
          return 'blue'
        } else if (temperature >= 0 && temperature < 30) {
          return 'green'
        } else {
          return 'red'
        }
      }

      // API call
      try {
        forecasts = await service.fetchData()
      } catch (e) {
        showError = true
        errorMessage = `Error while loading weather forecast: ${e.message}.`
      }

      // return data for the template
      return {
        // "properties"
        showError: showError,
        errorMessage: errorMessage,
        headers: headers,
        forecasts: forecasts,

        // methods
        getColor
      }
    }
  }
</script>

<style lang="scss" scoped>
</style>
