<template>
    <h1>Tests</h1>

    <button v-tooltip.right="'asd'">asd</button>

    {{myArray}}
    <draggable v-model="myArray" item-key="id">
        <template #item="{element}">
            <div>
                asdasd
                {{element.name}}
            </div>
        </template>
    </draggable>
</template>

<script setup lang="ts">
import draggable from "vuedraggable";
import {ref} from 'vue';

const myArray = ref([
    {id: 1, name: 'Max'},
    {id: 2, name: 'Anna'},
    {id: 3, name: 'Tom'},
]);
</script>