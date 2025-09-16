<template>
  <div class="p-6">
    <h1 class="text-2xl font-bold mb-6">My Notes</h1>

    <input
      v-model="search"
      type="text"
      placeholder="Search notes..."
      class="px-4 py-2 border rounded w-full mb-6"
    />

    <div v-if="loading">Loading...</div>
    <div v-else-if="filteredNotes.length === 0">No notes found.</div>

    <div v-else class="flex flex-wrap gap-6">
      <NoteWrapper
        v-for="note in filteredNotes"
        :key="note.id"
        :note="note"
        @delete="deleteNote"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import api from '../api/axios'
import NoteContainer from '../components/'

interface Note {
  id: number
  title: string
  content: string | null
  createdAt: string
}

const notes = ref<Note[]>([])
const loading = ref(false)
const search = ref('')

async function fetchNotes() {
  loading.value = true
  try {
    const res = await api.get('/Notes')
    notes.value = res.data as Note[]
  } catch (error) {
    console.error('Error fetching notes:', error)
  } finally {
    loading.value = false
  }
}

async function deleteNote(id: number) {
  const confirmDelete = confirm('Are you sure you want to delete this note?')
  if (!confirmDelete) return

  try {
    await api.delete(`/Notes/${id}`)
    notes.value = notes.value.filter(note => note.id !== id)
  } catch (error) {
    console.error('Failed to delete note:', error)
    alert('Failed to delete note.')
  }
}

const filteredNotes = computed(() =>
  notes.value.filter(note =>
    note.title.toLowerCase().includes(search.value.toLowerCase())
  )
)

onMounted(fetchNotes)
</script>

<style scoped>
/* Optional: ensure flex-wrap has some spacing */
</style>
