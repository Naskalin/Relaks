<template>
    
    <q-page-sticky position="bottom-right" :offset="fabPos">
        <div class="q-pa-md" style="width: 350px" v-touch-pan.prevent.mouse="moveFab">
            <q-list bordered>
                <q-expansion-item
                    group="somegroup"
                    icon="explore"
                    label="First"
                    default-opened
                    header-class="text-primary"
                >
                    <q-card>
                        <q-card-section>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem, eius reprehenderit eos corrupti
                            commodi magni quaerat ex numquam, dolorum officiis modi facere maiores architecto suscipit iste
                            eveniet doloribus ullam aliquid.
                        </q-card-section>
                    </q-card>
                </q-expansion-item>

                <q-separator />

                <q-expansion-item group="somegroup" icon="perm_identity" label="Second" header-class="text-teal">
                    <q-card>
                        <q-card-section>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem, eius reprehenderit eos corrupti
                            commodi magni quaerat ex numquam, dolorum officiis modi facere maiores architecto suscipit iste
                            eveniet doloribus ullam aliquid.
                        </q-card-section>
                    </q-card>
                </q-expansion-item>

                <q-separator />

                <q-expansion-item group="somegroup" icon="shopping_cart" label="Third" header-class="text-purple">
                    <q-card>
                        <q-card-section>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem, eius reprehenderit eos corrupti
                            commodi magni quaerat ex numquam, dolorum officiis modi facere maiores architecto suscipit iste
                            eveniet doloribus ullam aliquid.
                        </q-card-section>
                    </q-card>
                </q-expansion-item>

                <q-separator />

                <q-expansion-item
                    group="somegroup"
                    icon="bluetooth"
                    label="Fourth"
                    header-class="bg-teal text-white"
                    expand-icon-class="text-white"
                >
                    <q-card class="bg-teal-2">
                        <q-card-section>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quidem, eius reprehenderit eos corrupti
                            commodi magni quaerat ex numquam, dolorum officiis modi facere maiores architecto suscipit iste
                            eveniet doloribus ullam aliquid.
                        </q-card-section>
                    </q-card>
                </q-expansion-item>
            </q-list>
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