<template>
  <div class="p-6 max-w-xl mx-auto">
    <!-- Back Button -->
    <button
      @click="goBack"
      class="mb-4 text-sm text-blue-600 hover:underline flex items-center"
    >
      ← Back
    </button>

    <!-- Note Content -->
    <div v-if="loading">Loading...</div>
    <div v-else-if="!note">Note not found.</div>
    <div v-else>
      <h1 class="text-2xl font-bold mb-4">{{ note.title }}</h1>
      <p class="text-sm text-gray-500">Created: {{ formatDate(note.createdAt) }}</p>
      <p v-if="note.updatedAt" class="text-sm text-gray-500">Updated: {{ formatDate(note.updatedAt) }}</p>
      <div class="mt-4 text-gray-800 whitespace-pre-wrap">{{ note.content }}</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import {
