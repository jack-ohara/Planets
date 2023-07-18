type Planet = {
  id: string;
  name: string;
}

export type HomePageDataLoaderResponse = {
  planets: Planet[]
}

export async function homePageDataLoader(): Promise<HomePageDataLoaderResponse> {
  const response = await fetch(`${import.meta.env.VITE_API_URL as string}/Planets`);

  if (!response.ok) throw new Error('Error fetching all planets');

  const planets = (await response.json()) as Planet[];

  return { planets }
}