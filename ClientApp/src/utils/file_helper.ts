// https://stackoverflow.com/a/12900504/5638975

export function getFileExtension(path: string): string {
    return path.slice((path.lastIndexOf(".") - 1 >>> 0) + 2);
}