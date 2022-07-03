<template>
    <div>
        <div class="text-h6 q-mb-md">Наборы данных</div>
        <q-list v-if="aboutStore.list.length" bordered separator class="bg-my-grey">
            <q-item 
                v-for="eInfo in aboutStore.list" 
                clickable 
                @click="onClickItem(eInfo)"
            >
                <q-item-section avatar>
                    <q-icon name="las la-hand-point-right" />
                </q-item-section>
                <q-item-section class="q-pr-none">
                    <q-item-label :lines="3">{{ getCustomTitle(eInfo) }}</q-item-label>
                </q-item-section>
            </q-item>
        </q-list>
    </div>
</template>

<script setup lang="ts">
import {useEntryAboutStore} from "./entry_about_store";
import {EntryInfo, CustomInfo} from "../../../api/api_types";
import { scroll } from 'quasar'

const aboutStore = useEntryAboutStore();
const getCustomTitle = (eInfo: EntryInfo) => {
    if (eInfo.title) return eInfo.title;
    const custom = eInfo.info as CustomInfo;
    const firstGroup = custom.groups[0];
    if (firstGroup.title) return firstGroup.title;

    const firstItem = firstGroup.items[0];
    return firstItem.value;
}
const onClickItem = (eInfo: EntryInfo) => {
    const { getScrollTarget, setVerticalScrollPosition } = scroll
    const el = document.getElementById('eInfo_custom_'+eInfo.id) as HTMLElement;
    // console.log(el)
    const target = getScrollTarget(el);
    const offset = el.offsetTop
    console.log(offset);
    setVerticalScrollPosition(target, offset, 300);
    
    // const offset = el.offsetTop
    // const duration = 1000
    // setVerticalScrollPosition(target, offset, duration)
    // $router.push({name: 'entry-about', params: {entryId: $route.params.entryId}, hash: '#eInfo_custom_'+eInfo.id})
}
</script>