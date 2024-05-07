export class PostFeatureModel {
    postFeature: PostDto = new PostDto();
    postViews: PostDto[] = []
}

export class PostDto {
    id: number | undefined;
    title!: string;
    metaTitle: string | undefined;
    categoryName: string | undefined;
    image: string | undefined;
    content: string | undefined;
    deleteAt: Date | undefined;
    slug: string | undefined;
}
  