import axios from 'axios'
import { Forecast } from '../models/forecast'

export default {
  async fetchData () {
    try {
      const response = await axios.get('http://localhost:22487/api/WeatherForecast/')
    } catch (error) {
      throw new Error(error.reponse.data)
    }
  }
}
