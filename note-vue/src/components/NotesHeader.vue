<template>
  <div>
    <div class="flex items-center justify-between mb-4 space-x-4">
      <h1 class="text-2xl font-bold" style="color: #34699A;">📚 WE NOTES APP</h1>

      <div class="flex items-center space-x-12 flex-grow max-w-6xl">
        <input
          :value="search"
          @input="updateSearch"
          type="text"
          placeholder="🔍 Search notes..."
          class="px-4 py-2 border rounded-xl flex-grow"
        />

        <select
          v-model="localSortBy"
          @change="updateSort"
          class="border rounded px-2 py-2"
          aria-label="Sort notes"
        >
          <option value="title">Title (A-Z)</option>
          <option value="createdAt">Created Date (Newest)</option>
        </select>
      </div>

      <div class="relative flex items-center flex-shrink-0">
        <img
          @click="toggleDropdown"
          :src="`https://ui-avatars.com/api/?name=${username}`"
          alt="User Avatar"
          class="w-10 h-10 rounded-full border cursor-pointer"
          title="Click to logout"
        />

        <div
          v-if="dropdownOpen"
          @click.outside="closeDropdown"
          class="absolute right-0 mt-12 w-32 bg-white border rounded shadow-lg py-2 z-50"
        >
          <button
            @click="logout"
            class="w-full text-left px-4 py-2 hover:bg-red-100 text-red-600 font-semibold"
          >
            Logout
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits, ref, watch, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const props = defineProps<{ search: string; sortBy: string }>()
const emit = defineEmits(['update:search', 'update:sortBy'])

const localSortBy = ref(props.sortBy)
const username = ref('Guest')
const dropdownOpen = ref(false)

const router = useRouter()

watch(() => props.sortBy, (val) => {
  localSortBy.value = val
})

function updateSearch(event: Event) {
  const target = event.target as HTMLInputElement
  emit('update:search', target.value)
}

function updateSort() {
  emit('update:sortBy', localSortBy.value)
}

function toggleDropdown() {
  dropdownOpen.value = !dropdownOpen.value
}

function closeDropdown() {
  dropdownOpen.value = false
}

function logout() {
  localStorage.removeItem('token')
  localStorage.removeItem('username')
  
  dropdownOpen.value = false
  router.push('/login')
}

onMounted(() => {
  const storedName = localStorage.getItem('username')
  if (storedName) {
    username.value = storedName
  }
})
</script>

<style scoped>
</style>
