<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-200" style="color: #CBDCEB;">
    <div class="bg-white-100 p-8 rounded shadow-md w-full max-w-sm min-h-100 bg-gradient-to-b from-[#D2E0FB] to-[#ffffff] ">
      <h1 class="text-2xl mb-4 font-bold text-center text-gray-900">📚 WE NOTE</h1>
      <h1 class="text-lg font-semibold text-center text-gray-900">Create an account</h1>
      <h1 class="text-md font-normal mb-6 text-center text-gray-700">A Few step to create an account</h1>

      <form @submit.prevent="handleRegister">
        <div class="mb-4">
          <input
            v-model="username"
            type="text"
            class="w-full px-3 py-2 border rounded text-gray-600"
            required
            placeholder="👥Username"
          />
        </div>

        <div class="mb-6">
          <input
            v-model="password"
            type="password"
            class="w-full px-3 py-2 border rounded text-gray-600"
            required
            placeholder="🔒 Password"
          />
        </div>

        <button
          type="submit"
          class="w-full bg-blue-400 text-white py-2 px-4 rounded hover:bg-green-700"
          :disabled="loading"
        >
          {{ loading ? 'Registering...' : 'Register' }}
        </button>

        <p v-if="error" class="text-red-600 text-sm mt-4">{{ error }}</p>
      </form>

      <p class="mt-4 text-center text-sm text-gray-500">
        Already have an account?
        <router-link to="/login" class="text-blue-500 hover:underline">Login here</router-link>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '../api/axios.ts'

const router = useRouter()
const username = ref('')
const password = ref('')
const loading = ref(false)
const error = ref('')

async function handleRegister() {
  loading.value = true
  error.value = ''

  try {
    const response = await api.post('/Auth/register', {
      username: username.value,
      password: password.value,
    })

    const { token, userId, username: returnedUsername } = response.data

    localStorage.setItem('token', token)
    localStorage.setItem('userId', userId)
    localStorage.setItem('username', returnedUsername)

    router.push('/')
  } catch (err: any) {
    error.value = err.response?.data?.message || 'Registration failed. Try again.'
  } finally {
    loading.value = false
  }
}
</script>
