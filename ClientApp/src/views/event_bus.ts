import mitt, {Emitter} from 'mitt';

type Events = {
    rightSidebarScrollTop?: null;
};

export const emitter: Emitter<Events> = mitt<Events>();