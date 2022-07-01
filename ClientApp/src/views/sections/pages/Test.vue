<template>
    <div class="row">
        <div class="col-3">
            <div class="js-sticky" ref="sidebarEl">
                <!--            <div class="js-sticky" ref="sidebarEl" :data-margin-top="offsetTop" data-sticky-for="1023" data-sticky-class="is-sticky">-->
                Lorem ipsum dolor sit amet, consectetur adipisicing
                elit, sed do eiusmod tempor incididunt ut labore et
                dolore magna aliqua.

            </div>
        </div>
        <div class="col-9">
            <p v-for="n in 50" :key="n">
                Lorem ipsum dolor sit amet, consectetur adipisicing
                elit, sed do eiusmod tempor incididunt ut labore et
                dolore magna aliqua.
            </p>
        </div>
    </div>

    <p v-for="n in 50" :key="n">
        Lorem ipsum dolor sit amet, consectetur adipisicing
        elit, sed do eiusmod tempor incididunt ut labore et
        dolore magna aliqua.
    </p>

    <!--    <entry-select-field :entry-selected-id="entryId" v-model:is-show="isShow"/>-->
    <!--    <q-btn label="show" @click="isShow = !isShow"/>-->
    <!--    <p>{{entryId}}</p>-->
    <!--    <p>{{isShow}}</p>-->
</template>

<script setup lang="ts">
import {onMounted, computed, ref} from "vue";
import EntrySelectField from '../../fields/EntrySelect.Field.vue';

const sidebarEl = ref<HTMLElement | null>(null);

onMounted(() => {
    let offsetTop = 50;
    if (sidebarEl.value) {
        offsetTop = 50 + sidebarEl.value.offsetTop;
    }
    
})
// const Sticky = require('sticky-js');
// declare let Sticky: any;
// const sticky = new Sticky('.sticky');
// const isShow = ref(false);
// const entryId = ref('');

</script>