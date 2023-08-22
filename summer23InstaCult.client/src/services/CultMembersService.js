import { AppState } from "../AppState.js"
import { Cultist } from "../models/Cultist.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class CultMembersService {

  async getCultistsByCultId(cultId) {
    const res = await api.get(`api/cults/${cultId}/cultMembers`)
    logger.log('GOT CULTISTS', res.data)
    const newCultists = res.data.map(cultistPojo => new Cultist(cultistPojo))
    AppState.cultists = newCultists
  }

  async joinCult(cultId) {
    // const cultData = { cultId : cultId }
    const cultData = { cultId }
    const res = await api.post('api/cultMembers', cultData)
    logger.log('JOINED CULT', res.data)

    // NOTE please don't use this as a reference
    const frankensteinCultist = { ...AppState.account, cultMemberId: res.data.id }

    const newCultist = new Cultist(frankensteinCultist)

    AppState.cultists.push(newCultist)

  }

  async removeCultist(cultMemberId) {
    const res = await api.delete(`api/cultMembers/${cultMemberId}`)
    logger.log('REMOVED FROM CULT', res.data)
    const cultistIndex = AppState.cultists.findIndex(cultist => cultist.cultMemberId == cultMemberId)
    if (cultistIndex == -1) {
      throw new Error(`This id is bad: ${cultMemberId}`)
    }

    AppState.cultists.splice(cultistIndex, 1)

  }
}

export const cultMembersService = new CultMembersService()