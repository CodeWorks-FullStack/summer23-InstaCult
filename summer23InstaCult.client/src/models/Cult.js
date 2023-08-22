import { Profile } from "./Account.js"
import { RepoItem } from "./RepoItem.js"

export class Cult extends RepoItem {
  constructor (data) {
    super(data)
    // this.id = data.id
    // this.createdAt = new Date(data.createdAt)
    // this.updatedAt = new Date(data.updatedAt)
    this.name = data.name
    this.cultistCount = data.cultistCount
    this.description = data.description
    this.fee = data.fee
    this.coverImg = data.coverImg
    this.leaderId = data.leaderId
    this.leader = new Profile(data.leader)
  }
}

