<template>
    <p v-for="n in 30">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad, assumenda beatae consequatur culpa dignissimos
        eaque eligendi, error excepturi, fuga hic id in magni minus molestias mollitia praesentium reiciendis saepe
        sed!</p>

    <q-page-sticky position="top" :offset="[0, 18]">
        <div style="width: 250px">
            <p v-for="n in 15">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto et excepturi porro quibusdam
                soluta? Aliquam beatae consequatur dolores eius, enim laboriosam maiores minima minus obcaecati odit
                quam quis quisquam suscipit.</p>
        </div>
    </q-page-sticky>
</template>

<script>
import { ref } from 'vue'

export default {
    setup () {
        const fabPos = ref([ 18, 18 ])
        const draggingFab = ref(false)

        return {
            fabPos,
            draggingFab,

            onClick () {
                // console.log('Clicked on a fab action')
            },

            moveFab (ev) {
                draggingFab.value = ev.isFirst !== true && ev.isFinal !== true

                fabPos.value = [
                    fabPos.value[ 0 ] - ev.delta.x,
                    fabPos.value[ 1 ] - ev.delta.y
                ]
            }
        }
    }
}
</script>