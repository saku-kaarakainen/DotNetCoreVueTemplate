import axios from 'axios'
import { ref, watchEffect } from 'vue'
import { Forecast } from '../models/forecast'

export function weatherForecast() {
  const forecasts = ref<Array<Forecast>>(new Array<Forecast>())

  watchEffect(() => {
    axios.get('https://localhost:44332/api/WeatherForecast')
      .then(r => {
        forecasts.value = r.data
      })
  })

  return {
    forecasts
  }
}
