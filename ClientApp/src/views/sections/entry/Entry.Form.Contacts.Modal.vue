<template>
    <modal v-model:is-show="isShow" title="Контактные данные">
        фыв
    </modal>
    <div class="row">
        <div class="col">
            <div class="text-subtitle1">asd</div>
        </div>
        <div class="col"></div>
        <div class="col"></div>
    </div>
</template>

<script setup lang="ts">
import Modal from '../../components/Modal.vue';

import {ref} from 'vue';

const isShow = ref(false);
</script>
