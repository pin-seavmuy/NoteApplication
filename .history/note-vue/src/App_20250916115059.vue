<script setup lang="ts">
import HelloWorld from './components/HelloWorld.vue'
import NotesList from '../components/NotesList.vue'
import NotesHeader from '../components/NotesHeader.vue'  <!-- Import here -->
</script>

<template>
  <div>
    <nav class="p-4 bg-gray-100 flex space-x-4">
      <router-link to="/" class="text-blue-600 hover:underline">Notes</router-link>
      <router-link to="/create" class="text-blue-600 hover:underline">Create Note</router-link>
    </nav>

    <!-- Example of using NotesHeader here (adjust placement as needed) -->
    <NotesHeader v-model:search="search" />

    <router-view />
  </div>
</template>
