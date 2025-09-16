<template>
  <div class="p-6">
    <h1 class="text-2xl text-red-300 font-bold mb-4">My Notes</h1>

    <input
      v-model="search"
      type="text"
      placeholder="Search notes..."
      class="px-4 py-2 border rounded w-full mb-4"
    />

    <div v-if="loading">Loading...</div>
    <div v-else-if="filteredNotes.length === 0">No notes found.</div>

    <div v-else class="space-y-4">
      <div
        v-for="note in filteredNotes"
        :key="note.id"
        class="p-4 border rounded shadow hover:bg-gray-50 relative group"
      >
        <!-- Title -->
        <h2 class="font-semibold cursor-pointer" @click="goToDetail(note.id)">
          {{ note.title }}
        </h2>
        <!-- Created Date -->
        <p class="text-sm text-gray-500">{{ formatDate(note.createdAt) }}</p>
        <!-- Content -->
        <p class="mt-2 text-gray-700">{{ note.content }}</p>

        <!-- Delete Button -->
        <button
          @click.stop="deleteNote(note.id)"
          class="absolute top-2 right-2 text-red-500 hover:text-red-700 text-sm hidden group-hover:block"
        >
          ✕
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import api from '../api/axios'

interface Note {
  id: number
  title: string
  content: string | null
  createdAt: string
}

const notes = ref<Note[]>([])
const loading = ref(false)
const search = ref('')
const router = useRouter()

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
    console.error('Error deleting note:', error)
    alert('Failed to delete note.')
  }
}

function goToDetail(id: number) {
  router.push(`/notes/${id}`)
}

const filteredNotes = computed(() =>
  notes.value.filter(note =>
    note.title.toLowerCase().includes(search.value.toLowerCase())
  )
)

function formatDate(dateStr: string) {
  return new Date(dateStr).toLocaleString()
}

onMounted(fetchNotes)
</script>
