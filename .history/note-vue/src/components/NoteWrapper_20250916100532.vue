<template>
  <div class="note-wrapper p-4 border rounded shadow hover:shadow-lg transition cursor-pointer" @click="goToDetail">
    <div class="flex justify-between items-start mb-2">
      <h3 class="font-semibold text-lg">{{ note.title }}</h3>
      <div class="flex space-x-2">
        <button
          @click.stop="goToEdit"
          aria-label="Edit note"
          class="text-blue-500 hover:text-blue-700"
        >
          ✏️
        </button>
        <button
          @click.stop="handleDelete"
          aria-label="Delete note"
          class="text-red-500 hover:text-red-700"
        >
          🗑️
        </button>
      </div>
    </div>
    <p class="text-gray-700 text-sm whitespace-pre-wrap">{{ note.content }}</p>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue'
import { useRouter } from 'vue-router'

interface Note {
  id: number
  title: string
  content: string | null
  createdAt: string
}

const props = defineProps<{
  note: Note
}>()

const emits = defineEmits<{
  (e: 'delete', id: number): void
}>()

const router = useRouter()

function goToDetail() {
  router.push({ name: 'NoteDetail', params: { id: props.note.id } })
}

function goToEdit() {
  router.push({ name: 'UpdateNote', params: { id: props.note.id } })
}

function handleDelete() {
  emits('delete', props.note.id)
}
</script>

<style scoped>
.note-wrapper {
  width: 300px; /* Fixed width for grid style */
  min-height: 150px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
</style>
