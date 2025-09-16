import { defineStore } from 'pinia'
import api from '@/api/axios'

export interface Note {
  id: number
  title: string
  content?: string
  createdAt: string
  updatedAt?: string
}

export const useNoteStore = defineStore('note', {
  state: () => ({
    notes: [] as Note[],
    loading: false,
    error: null as string | null
  }),

  actions: {
    async fetchNotes() {
      this.loading = true
      try {
        const res = await api.get('/notes')
        this.notes = res.data
      } catch (err) {
        this.error = 'Failed to fetch notes'
      } finally {
        this.loading = false
      }
    },

    async createNote(note: Partial<Note>) {
      const res = await api.post('/notes', note)
      this.notes.push(res.data)
    },

    async updateNote(id: number, note: Partial<Note>) {
      const res = await api.put(`/notes/${id}`, note)
      const i = this.notes.findIndex(n => n.id === id)
      if (i !== -1) this.notes[i] = res.data
    },

    async deleteNote(id: number) {
      await api.delete(`/notes/${id}`)
      this.notes = this.notes.filter(n => n.id !== id)
    }
  }
})
