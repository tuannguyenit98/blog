export class Category {
  id: number | undefined;
  createdAt: Date | undefined;
  updatedAt: Date | undefined;
  createdBy: number | undefined;
  updatedBy: number | undefined;
  deleteAt: Date | undefined;
  parentId: number | undefined;
  title!: string;
  metaTitle: string | undefined;
  slug: string | undefined;
  postNumber: number | undefined;
}
