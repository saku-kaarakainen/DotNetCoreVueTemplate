<template>
  <div v-if="showError">{{ errorMessage }}</div>

  <Suspense v-else>
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

    <template #fallback>
      <span>Loading...</span>
    </template>
  </Suspense>
</template>

<script lang="ts">
  import { computed } from 'vue'
  import { weatherForecast } from '../services/ApiService'

  export default {
    setup() {
      // Initializing properties
      var showError = false
      var errorMessage = 'Error while loading weather forecast'
      var headers = [
        { text: 'Date', value: 'date' },
        { text: 'Temp. (C)', value: 'temperatureC' },
        { text: 'Temp. (F)', value: 'temperatureF' },
        { text: 'Summary', value: 'summary' }
      ]

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
      const forecasts = computed(() => {
        return weatherForecast().forecasts
      })

      // return data for the template
      return {
        // "properties"
        showError: showError,
        errorMessage: errorMessage,
        headers: headers,

        // computed values
        forecasts: forecasts.value,

        // methods
        getColor
      }
    }
  }
</script>

<style lang="scss" scoped>
</style>
