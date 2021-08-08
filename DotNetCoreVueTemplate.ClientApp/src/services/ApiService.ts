import axios from 'axios'
import { Forecast } from '../models/forecast'

export class ApiService {
  async fetchData(): Promise<Array<Forecast>> {
    try {
      const response = await axios.get('https://localhost:44332/api/WeatherForecast')
      return response.data
    } catch (error) {
      throw new Error(error.reponse.data)
    }
  }
}
