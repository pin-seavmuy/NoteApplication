import { defineStore } from 'pinia';
import api from '@/api/axios';
import axios from 'axios';

export interface Note {
  id: number;
  title: string;
  content?: string;
  createdAt: string;
  updatedAt?: string;
}

export const useNoteStore = defineStore('notes', {
  state: () => ({
    notes: [] as Note[],
    loading: false,
    error: null as string | null
  }),
  actions: {
    async fetchNotes() {
      this.loading = true;
      try {
        const response = await api.get('/notes');
        this.notes = response.data;
      } catch (error: any) {
        this.error = error.message || 'Failed to fetch notes';
      } finally {
        this.loading = false;
      }
    },

    async addNote(note: Partial<Note>) {
      const response = await api.post('/notes', note);
      this.notes.push(response.data);
    },

    async updateNote(id: number, note: Partial<Note>) {
      const response = await api.put(`/notes/${id}`, note);
      const index = this.notes.findIndex(n => n.id === id);
      if (index !== -1) this.notes[index] = response.data;
    },

    async deleteNote(id: number) {
      await api.delete(`/notes/${id}`);
      this.notes = this.notes.filter(n => n.id !== id);
    }
  }
});
