export class PostFeatureModel {
    postFeature: PostDto = new PostDto();
    postViews: [] = []
}

export class PostDto {
    id: number | undefined;
    title!: string;
    metaTitle: string | undefined;
    categoryName: string | undefined;
    image: string | undefined;
    content: string | undefined;
}
  