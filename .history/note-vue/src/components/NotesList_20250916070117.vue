<template>
  <div class="p-6 max-w-2xl mx-auto">
    <h1 class="text-2xl font-bold mb-4">My Notes</h1>

    <!-- ✅ Create Note Form -->
    <form @submit.prevent="createNote" class="mb-6 space-y-4">
      <input
        v-model="newNote.title"
        type="text"
        placeholder="Note Title"
        class="px-4 py-2 border rounded w-full"
        required
      />
      <textarea
        v-model="newNote.content"
        placeholder="Note Content"
        class="px-4 py-2 border rounded w-full"
      ></textarea>
      <button
        type="submit"
        class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
      >
        Create Note
      </button>
    </form>

    <!-- 🔍 Search -->
    <input
      v-model="search"
      type="text"
      placeholder="Search notes..."
      class="px-4 py-2 border rounded w-full mb-4"
    />

    <!-- 🔄 Loading & Message -->
    <div v-if="loading">Loading...</div>
    <div v-else-if="filteredNotes.length === 0">No notes found.</div>

    <!-- 📋 Notes -->
    <div v-else class="space-y-4">
      <div
        v-for="note in filteredNotes"
        :key="note.id"
        class="p-4 border rounded shadow hover:bg-gray-50"
      >
        <h2 class="font-semibold">{{ note.title }}</h2>
        <p class="text-sm text-gray-500">{{ formatDate(note.createdAt) }}</p>
        <p class="mt-2 text-gray-700">{{ note.content }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue'
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
const newNote = ref({
  title: '',
  content: '',
})

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

async function createNote() {
  try {
    await api.post('/Notes', {
      title: newNote.value.title,
      content: newNote.value.content,
    })
    newNote.value.title = ''
    newNote.value.content = ''
    await fetchNotes() // refresh list
  } catch (error) {
    console.error('Error creating note:', error)
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
