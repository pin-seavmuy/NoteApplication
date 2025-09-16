<template>
  <div
    v-if="show"
    class="fixed inset-0 bg-w bg-opacity-50 flex items-center justify-center z-50"
    @click.self="close"
  >
    <div class="bg-white rounded-lg shadow-lg p-6 w-full max-w-md relative">
      <button
        @click="close"
        class="absolute top-3 right-3 text-gray-500 hover:text-gray-800 text-xl font-bold"
        aria-label="Close modal"
      >
        &times;
      </button>

      <h2 class="text-2xl font-bold mb-6">Create New Note</h2>

      <form @submit.prevent="handleSubmit" class="space-y-4">
        <div>
          <label for="title" class="block mb-1 font-semibold">Title</label>
          <input
            id="title"
            v-model="title"
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
            v-model="content"
            rows="5"
            placeholder="Write your note here..."
            class="w-full px-4 py-2 border rounded resize-none"
          ></textarea>
        </div>

        <button
          type="submit"
          :disabled="loading"
          class="bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700 disabled:opacity-50"
        >
          {{ loading ? 'Saving...' : 'Create Note' }}
        </button>
      </form>

      <p v-if="error" class="mt-4 text-red-600">{{ error }}</p>
      <p v-if="success" class="mt-4 text-green-600">Note created successfully!</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, defineEmits, watch } from 'vue'
import api from '../api/axios'

const props = defineProps<{ show: boolean }>()
const emit = defineEmits(['update:show', 'noteCreated'])

const title = ref('')
const content = ref('')
const loading = ref(false)
const error = ref<string | null>(null)
const success = ref(false)

watch(() => props.show, (newVal) => {
  if (!newVal) {
    // Reset form on close
    title.value = ''
    content.value = ''
    error.value = null
    success.value = false
    loading.value = false
  }
})

function close() {
  emit('update:show', false)
}

async function handleSubmit() {
  if (!title.value.trim()) {
    error.value = 'Title is required.'
    return
  }
  loading.value = true
  error.value = null
  success.value = false

  try {
    await api.post('/Notes', {
      title: title.value,
      content: content.value || null,
    })
    success.value = true
    emit('noteCreated')  // notify parent to refresh notes
    setTimeout(() => {
      close()
    }, 1000)
  } catch (err) {
    error.value = 'Failed to create note. Please try again.'
    console.error(err)
  } finally {
    loading.value = false
  }
}
</script>
