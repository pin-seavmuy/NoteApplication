<template>
  <div class="p-6">
    <h1 class="text-2xl font-bold mb-4">My Notes</h1>

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
        class="p-4 border rounded shadow hover:bg-gray-100"
      >
        <h2
          class="font-semibold cursor-pointer"
          @click="goToDetail(note.id)"
        >
          {{ note.title }}
        </h2>
        <p class="text-sm text-gray-500">{{ formatDate(note.createdAt) }}</p>

        <div class="mt-2 flex space-x-4">
          <button
            class="text-blue-500 hover:underline"
            @click.stop="goToEdit(note.id)"
          >
            Edit
          </button>
          <button
            class="text-red-500 hover:underline"
            @click.stop="deleteNote(note.id)"
          >
            Delete
          </button>
        </div>
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
  updatedAt?: string | null
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

function goToDetail(id: number) {
  router.push({ name: 'NoteDetail', params: { id } })
}

function goToEdit(id: number) {
  router.push({ name: 'UpdateNote', params: { id } })
}

async function deleteNote(id: number) {
  const confirmDelete = confirm('Are you sure you want to delete this note?')
  if (!confirmDelete) return

  try {
    await api.delete(`/Notes/${id}`)
    // Remove the deleted note from the list without re-fetching
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

function formatDate(dateStr: string) {
  return new Date(dateStr).toLocaleString()
}

onMounted(fetchNotes)
</script>
