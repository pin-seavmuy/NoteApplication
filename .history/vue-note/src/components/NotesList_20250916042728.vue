<template>
  <div class="p-6">
    <h1 class="text-2xl font-bold mb-4">My Notes</h1>

    <div class="mb-4">
      <input
        v-model="search"
        type="text"
        placeholder="Search notes..."
        class="px-4 py-2 border rounded w-full"
      />
    </div>

    <div v-if="loading">Loading...</div>
    <div v-else-if="filteredNotes.length === 0">No notes found.</div>

    <div class="space-y-4">
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
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import useNoteStore from '@/stores/noteStore'

const store = useNoteStore()
const search = ref('')

onMounted(() => {
  store.fetchNotes()
})

const filteredNotes = computed(() => {
  return store.notes.filter(note =>
    note.title.toLowerCase().includes(search.value.toLowerCase())
  )
})

const loading = computed(() => store.loading)

function formatDate(date: string) {
  return new Date(date).toLocaleString()
}
</script>
