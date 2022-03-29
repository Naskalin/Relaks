<template>
    <h1>Tests</h1>
    
    {{phone}}
    <phone-field v-model="phone"></phone-field>
</template>

<script setup lang="ts">
import PhoneField from '../../fields/Phone.Field.vue';
import {ref} from 'vue';

const phone = ref('');
</script>