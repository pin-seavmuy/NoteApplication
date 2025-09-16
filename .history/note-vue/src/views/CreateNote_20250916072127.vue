<template>
  <div class="p-6 max-w-md mx-auto">
    <h1 class="text-2xl font-bold mb-6">Create New Note</h1>

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
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '../api/axios'

const title = ref('')
const content = ref('')
const loading = ref(false)
const error = ref<string | null>(null)
const success = ref(false)

const router = useRouter()

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
    // Optionally, reset form
    title.value = ''
    content.value = ''
    
    // Navigate back to notes list after 1 second delay
    setTimeout(() => {
      router.push('/')
    }, 1000)
  } catch (err) {
    error.value = 'Failed to create note. Please try again.'
    console.error(err)
  } finally {
    loading.value = false
  }
}
</script>
