interface IBadgeItem {
    type: string;
    value: string;
}

interface ISaperator {
    name: string;
    type?: string;
}

interface ISubChildren {
    state: string;
    name: string;
    type?: string;
}

interface IChildrenItems {
    state: string;
    name: string;
    type?: string;
    child?: ISubChildren[];
}

export interface IMenu {
    state: string;
    name: string;
    type: string;
    icon: string;
    badge?: IBadgeItem[];
    saperator?: ISaperator[];
    children?: IChildrenItems[];
}