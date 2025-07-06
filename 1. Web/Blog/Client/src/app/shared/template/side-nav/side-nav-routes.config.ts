import { SideNavInterface } from "../../interfaces/side-nav.type";

export const ROUTES: SideNavInterface[] = [
    {
        path: '/dashboard/category/list',
        title: 'Danh mục',
        iconType: 'nzIcon',
        iconTheme: 'outline',
        icon: 'dashboard',
        submenu: [
        ]
    },
    {
        path: '/dashboard/post/list',
        title: 'Bài đăng',
        iconType: 'nzIcon',
        iconTheme: 'outline',
        icon: 'layout',
        submenu: [
        ]
    }
]    