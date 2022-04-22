<template>
<!--    <q-tooltip class="arrow-tooltip no-padding bg-transparent no-border-radius"-->
<!--               :anchor="basePosition.anchor[direction]"-->
<!--               :self="basePosition.self[direction]"-->
<!--               :offset="[0, 0]">-->
<!--        <div class="arrow-tooltip__container" :class="direction" :style="cssVars">-->
<!--            <div class="arrow-tooltip__arrow" :class="direction"></div>-->
<!--            <div class="arrow-tooltip__body">-->
<!--                <slot></slot>-->
<!--            </div>-->
<!--        </div>-->
<!--    </q-tooltip>-->
    <button v-tooltip="'asd'">asd</button>
</template>

<script setup lang="ts">
// import {withDefaults, computed} from "vue";
//
// declare type directionType = 'left' | 'right' | 'top' | 'bottom';
//
// const basePosition: {
//     anchor: { [index: string]: any },
//     self: { [index: string]: any }
// } = {
//     anchor: {
//         left: 'center left',
//         right: 'center right',
//         top: 'top middle',
//         bottom: 'bottom middle',
//     },
//     self: {
//         left: 'center right',
//         right: 'center left',
//         top: 'bottom middle',
//         bottom: 'top middle',
//     }
// }
// const props = withDefaults(defineProps<{
//     direction?: directionType,
//     arrowWidth?: string
//     fontSize?: string
// }>(), {
//     direction: 'right',
//     fontSize: '.9rem',
//     arrowWidth: '6px',
// });
// const cssVars = computed<any>(() => {
//     return {
//         '--font-size': props.fontSize,
//         '--arrow-width': props.arrowWidth,
//     }
// })
</script>

<style lang="scss">
//.arrow-tooltip {
//    min-width: 300px;
//    max-width: 500px;
//
//    .q-separator {
//        background: $grey-8;
//        margin: .5rem 0;
//    }
//
//    small {
//        text-transform: uppercase;
//        color: $grey-5;
//    }
//
//    &__container {
//        position: relative;
//
//        &.left {
//            padding-right: calc(var(--arrow-width) * 2);
//        }
//
//        &.right {
//            padding-left: calc(var(--arrow-width) * 2);
//        }
//
//        &.top {
//            padding-bottom: calc(var(--arrow-width) * 2);
//        }
//
//        &.bottom {
//            padding-top: calc(var(--arrow-width) * 2);
//        }
//    }
//
//    &__arrow {
//        content: "";
//        height: calc(var(--arrow-width) * 2);
//        width: calc(var(--arrow-width) * 2);
//        position: absolute;
//        pointer-events: none;
//        border: solid rgba(0, 0, 0, 0);
//
//        &.left {
//            right: 0;
//            top: 50%;
//            border-left-color: $secondary;
//            border-width: var(--arrow-width);
//            margin-top: calc(var(--arrow-width) * -1);
//        }
//
//        &.right {
//            left: 0;
//            top: 50%;
//            border-right-color: $secondary;
//            border-width: var(--arrow-width);
//            margin-top: calc(var(--arrow-width) * -1);
//        }
//
//        &.top {
//            bottom: 0;
//            left: 50%;
//            border-top-color: $secondary;
//            border-width: var(--arrow-width);
//            margin-left: calc(var(--arrow-width) * -1);
//        }
//
//        &.bottom {
//            top: 0;
//            left: 50%;
//            border-bottom-color: $secondary;
//            border-width: var(--arrow-width);
//            margin-left: calc(var(--arrow-width) * -1);
//        }
//    }
//
//    &__body {
//        background: $secondary;
//        padding: .5rem;
//        border-radius: $generic-border-radius;
//        font-size: var(--font-size);
//    }
//}
</style>