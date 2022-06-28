import {defineStore} from 'pinia';
import {StructureConnection, StructureConnectionDirection} from "../../../api/rerources/api_structure_connections";
import {apiStructureConnection} from "../../../api/rerources/api_structure_connections";
import LeaderLine from "leader-line-new";
import {useStructureStore} from "./structure_store";

const SIZE_DEFAULT = 2;
const SIZE_ACTIVE = 5;
const COLOR_DEFAULT = '#ff8a652b';

const COLOR_IN = '#4D964D';
const COLOR_OUT = '#3f51b5';
const COLOR_DOUBLE = '#f4511e';
const getLineColor = (arrow: ArrowType) => {
    switch (arrow) {
        case "in":
            return COLOR_IN;
        case "out":
            return COLOR_OUT;
        case "double":
            return COLOR_DOUBLE;
    }
}
export declare type StructureConnectionWithArrow = {
    arrow: ArrowType
} & StructureConnection;

declare type StructureConnectionsStoreState = {
    activeConnectionId: string | null,
    connections: StructureConnection[],
    connectionLines: { [index: string]: LeaderLine },
    isShowLines: boolean
}

export declare type ArrowType = 'in' | 'out' | 'double';
export function getConnectionArrowType(structureSelectedId: string, connection: StructureConnection): ArrowType {
    const isFirst = connection.structureFirstId !== structureSelectedId;
    switch (connection.direction) {
        case "Normal":
            return isFirst ? 'in' : 'out';
        case "Reverse":
            return isFirst ? 'out' : 'in';
        case "Bidirectional":
            return 'double';
    }
}


function updateLineDirection(direction: StructureConnectionDirection, options: LeaderLine.Options): LeaderLine.Options {
    switch (direction) {
        case "Normal":
            options.startPlug = 'disc';
            options.endPlug = 'arrow1';
            options.endSocket = 'left';
            break;
        case "Reverse":
            options.startPlug = 'arrow1';
            options.endPlug = 'disc';
            options.startSocket = 'left';
            break;
        case "Bidirectional":
            options.startPlug = 'arrow1';
            options.endPlug = 'arrow1';
            options.startSocket = 'left';
            options.endSocket = 'left';
            break;
    }

    return options;
}

export const useStructureConnectionsStore = defineStore('StructureConnectionsStore', {
    state: (): StructureConnectionsStoreState => {
        return {
            activeConnectionId: null,
            connections: [],
            connectionLines: {},
            isShowLines: true
        }
    },
    getters: {
        structureConnections: (state): StructureConnectionWithArrow[] => {
            const structureSelectedId = (useStructureStore()).structureSelectedId;
            if (!state.connections.length || !structureSelectedId) return [];
            const arr: StructureConnectionWithArrow[] = [];
            state.connections.forEach(x => {
                if (x.structureFirstId === structureSelectedId || x.structureSecondId === structureSelectedId) {
                    arr.push({
                        ...x,
                        arrow: getConnectionArrowType(structureSelectedId, x),
                    });
                } 
            });
            
            return arr;
        }
    },
    actions: {
        hideLines() {
            this.isShowLines = false;
            Object.keys(this.connectionLines).forEach(key => {
                this.connectionLines[key].hide()
            })
        },
        showLines() {
            this.isShowLines = true;
            Object.keys(this.connectionLines).forEach(key => {
                this.connectionLines[key].show()
            })
        },
        async getConnectionsAsync(entryId: string) {
            this.connections = await apiStructureConnection.list({entryId: entryId});
        },
        drawActiveConnection(oldActiveConnectionId: string | null) {
            if (this.activeConnectionId) {
                const line = this.connectionLines[this.activeConnectionId] || null;
                if (!line) return;
                // line.color = COLOR_ACTIVE_CONNECTION;
                line.size = SIZE_ACTIVE;
            }
            
            if (!oldActiveConnectionId) return;
            const oldLine = this.connectionLines[oldActiveConnectionId] || null;
            if(!oldLine) return;
            
            // let changeColorTo = COLOR_DEFAULT;
            // for (const [index, connection] of this.structureConnections.entries()) {
            //     if (connection.id === oldActiveConnectionId) {
            //         changeColorTo = getLineColor(connection.arrow);
            //         break;
            //     }
            // }
            // // oldLine.changeColorTo = activeStructureConnectionIds.includes(oldActiveConnectionId) ? COLOR_ACTIVE_STRUCTURE : COLOR_DEFAULT;
            // oldLine.color = changeColorTo;
            oldLine.size = SIZE_DEFAULT;
        },
        drawActiveStructureConnections(structureId: string | null, oldStructureId: string | null) {
            // Сбрасываем активную выделенную связь
            this.activeConnectionId = null;
            
            // Старые связи перекрашиваем в дефолтный цвет
            if (this.connections.length && oldStructureId && oldStructureId !== '') {
                this.connections.forEach(connection => {
                    // Это более не активные связи => дефолтное состояние
                    if (connection.structureFirstId === oldStructureId || connection.structureSecondId === oldStructureId) {
                        const line = this.connectionLines[connection.id] || null;
                        if (line) {
                            line.color = COLOR_DEFAULT;
                            line.size = SIZE_DEFAULT;
                        }
                    }
                })
            }
            
            // Выделяем новые связи структуры
            if (!this.structureConnections.length) return;
            this.structureConnections.forEach(connection => {
                const line = this.connectionLines[connection.id] || null;
                if (line) {
                    // line.color = COLOR_ACTIVE_STRUCTURE;
                    line.color = getLineColor(connection.arrow);
                    line.size = SIZE_DEFAULT;
                }
            })
        },
        drawConnections(connections: StructureConnection[], options = {color: COLOR_DEFAULT, size: SIZE_DEFAULT}, connectionIdPrefix = '') {
            const structuresEls = Array.from(document.querySelectorAll('.js-structure-connections'));
            if (!structuresEls.length) return;

            connections.forEach(connection => {
                let connectionItem: { [index: string]: Element } = {};

                [
                    {key: 'first', structureId: connection.structureFirstId},
                    {key: 'second', structureId: connection.structureSecondId}
                ].forEach(item => {
                    const findStructuresEls = structuresEls.filter(el => el.id === item.structureId);
                    if (!findStructuresEls.length) {
                        throw new Error('structureId is missing for StructureConnection');
                    }
                    const boxEl = findStructuresEls[0];
                    const connectionEl = document.createElement('div');
                    connectionEl.id = connectionIdPrefix + item.key + '_' + connection.id;
                    connectionEl.classList.add('structure-connection__el', item.key, connection.direction.toLocaleLowerCase());

                    boxEl.appendChild(connectionEl);
                    connectionItem[item.key] = connectionEl;
                })
                updateLineDirection(connection.direction, options);
                this.connectionLines[connection.id] = new LeaderLine(connectionItem.first, connectionItem.second, options);
            })
        }
    }
})