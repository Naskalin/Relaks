import mitt, {Emitter} from 'mitt';

type Events = {
    rightSidebarScrollTop?: null;
    scrollToAnchor: string // Element Id
};

export const emitter: Emitter<Events> = mitt<Events>();