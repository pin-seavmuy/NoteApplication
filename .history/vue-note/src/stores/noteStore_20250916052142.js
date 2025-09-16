import { reactive } from 'vue'
import apiClient from '@/api/axios'

export const noteStore = reactive({
  notes: [],
  loading: false,

  async fetchNotes() {
  this.loading = true
  try {
    const response = await apiClient.get('/Notes', { params: { asc: false } })
    this.notes = response.data
  } catch (error) {
    console.error('Error fetching notes:', error)
  } finally {
    this.loading = false
  }
}


  async createNote(note) {
    try {
      const response = await apiClient.post('/Notes', note)
      this.notes.push(response.data)
    } catch (error) {
      console.error('Error creating note:', error)
    }
  },

  async updateNote(note) {
    try {
      await apiClient.put(`/notes/${note.id}`, note)
      const index = this.notes.findIndex(n => n.id === note.id)
      if (index !== -1) this.notes[index] = note
    } catch (error) {
      console.error('Error updating note:', error)
    }
  },

  async deleteNote(id) {
    try {
      await apiClient.delete(`/notes/${id}`)
      this.notes = this.notes.filter(n => n.id !== id)
    } catch (error) {
      console.error('Error deleting note:', error)
    }
  },
})
