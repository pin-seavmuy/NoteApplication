<template>
  <div class="p-6">
    <h1 class="text-2xl font-bold mb-4 flex justify-between items-center">
      My Notes
      <button
        @click="showModal = true"
        class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
      >
        + New Note
      </button>
    </h1>

    <!-- Search input and notes list remain unchanged -->
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
        class="p-4 border rounded shadow hover:bg-gray-50 cursor-pointer"
      >
        <h2 class="font-semibold">{{ note.title }}</h2>
        <p class="text-sm text-gray-500">{{ formatDate(note.createdAt) }}</p>
        <p class="mt-2 text-gray-700">{{ note.content }}</p>
      </div>
    </div>

    <!-- Modal Overlay -->
    <div
      v-if="showModal"
      class="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50"
    >
      <!-- Modal content -->
      <div class="bg-white rounded-lg p-6 w-full max-w-md shadow-lg relative">
        <button
          @click="closeModal"
          class="absolute top-2 right-2 text-gray-600 hover:text-gray-900 text-xl font-bold"
          aria-label="Close modal"
        >
          &times;
        </button>

        <h2 class="text-xl font-bold mb-4">Create New Note</h2>

        <form @submit.prevent="handleCreateNote" class="space-y-4">
          <div>
            <label for="title" class="block mb-1 font-semibold">Title</label>
            <input
              id="title"
              v-model="newTitle"
              type="text"
              required
              placeholder="Note title"
              class="w-full px-4 py-2 border rounded"
            />
          </div>

          <div>
            <label for="content" class="block mb-1 font-semibold">Content</label>
            <textarea
              id="content"
              v-model="newContent"
              rows="4"
              placeholder="Write your note here..."
              class="w-full px-4 py-2 border rounded resize-none"
            ></textarea>
          </div>

          <div class="flex justify-end space-x-2">
            <button
              type="button"
              @click="closeModal"
              class="px-4 py-2 border rounded hover:bg-gray-100"
            >
              Cancel
            </button>
            <button
              type="submit"
              :disabled="loadingCreate"
              class="bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700 disabled:opacity-50"
            >
              {{ loadingCreate ? 'Saving...' : 'Create' }}
            </button>
          </div>

          <p v-if="errorCreate" class="mt-2 text-red-600">{{ errorCreate }}</p>
        </form>
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

const showModal = ref(false)
const newTitle = ref('')
const newContent = ref('')
const loadingCreate = ref(false)
const errorCreate = ref('')

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

const filteredNotes = computed(() =>
  notes.value.filter(note =>
    note.title.toLowerCase().includes(search.value.toLowerCase())
  )
)

function formatDate(dateStr: string) {
  return new Date(dateStr).toLocaleString()
}

function closeModal() {
  showModal.value = false
  newTitle.value = ''
  newContent.value = ''
  errorCreate.value = ''
}

async function handleCreateNote() {
  if (!newTitle.value.trim()) {
    errorCreate.value = 'Title is required.'
    return
  }

  loadingCreate.value = true
  errorCreate.value = ''

  try {
    await api.post('/Notes', {
      title: newTitle.value,
      content: newContent.value || null,
    })
    closeModal()
    fetchNotes() // Refresh list after create
  } catch (err) {
    errorCreate.value = 'Failed to create note. Please try again.'
    console.error(err)
  } finally {
    loadingCreate.value = false
  }
}

onMounted(fetchNotes)
</script>
