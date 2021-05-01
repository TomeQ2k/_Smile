export const addClass = (element: string, className: string) => {
  document.querySelector(element).classList.add(className);
};

export const removeClass = (element: string, className: string) => {
  document.querySelector(element).classList.remove(className);
};

export const shortenStr = (str: string, end: number, start?: number) => {
  return str.slice(start ? start : 0, end);
};
