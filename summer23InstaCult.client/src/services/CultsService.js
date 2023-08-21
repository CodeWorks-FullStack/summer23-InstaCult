import { AppState } from "../AppState.js"
import { Cult } from "../models/Cult.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class CultsService {

  async getCults() {
    const res = await api.get('api/cults')
    logger.log('GOT CULTS', res.data)
    const newCults = res.data.map(cultPojo => new Cult(cultPojo))
    AppState.cults = newCults
  }

  async createCult(cultData) {
    const res = await api.post('api/cults', cultData)
    logger.log('CREATED CULT', res.data)
    const newCult = new Cult(res.data)
    AppState.cults.push(newCult)
  }

  async getCultById(cultId) {
    const res = await api.get(`api/cults/${cultId}`)
    logger.log('GOT CULT', res.data)
    const newCult = new Cult(res.data)
    AppState.activeCult = newCult
  }
}

export const cultsService = new CultsService()