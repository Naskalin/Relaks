// https://stackoverflow.com/a/12900504/5638975

export function getFileExtension(path: string): string {
    return path.slice((path.lastIndexOf(".") - 1 >>> 0) + 2);
}

export function randomId(): string {
    return new Date().getTime().toString(36).concat(performance.now().toString(), Math.random().toString()).replace(/\./g, "");
}