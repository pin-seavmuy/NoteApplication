<script setup lang="ts">
import Header from './components/'
</script>

<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Global Header -->
    <Header />

    <!-- Navigation (optional) -->
    <nav class="p-4 bg-gray-100 flex space-x-4 border-b">
      <router-link to="/" class="text-blue-600 hover:underline">Notes</router-link>
      <router-link to="/create" class="text-blue-600 hover:underline">Create Note</router-link>
    </nav>

    <!-- Page Content -->
    <router-view />
  </div>
</template>

<style scoped>
</style>
