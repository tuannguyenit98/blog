export class Post {
  id: number | undefined;
  createdAt: Date | undefined;
  updatedAt: Date | undefined;
  createdBy: number | undefined;
  updatedBy: number | undefined;
  deleteAt: Date | undefined;
  fK_UserId: number | undefined;
  fK_CategoryId: number | undefined;
  title!: string;
  metaTitle: string | undefined;
  image: string | undefined;
  content: string | undefined;
  status: string | undefined;
  comments: any[] = [];
  totalComment: number | undefined;
}
