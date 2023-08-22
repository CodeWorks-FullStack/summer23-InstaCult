export class RepoItem {
  constructor (data) {
    this.id = data.id
    this.updatedAt = new Date(data.updatedAt)
    this.createdAt = new Date(data.createdAt)
  }
}