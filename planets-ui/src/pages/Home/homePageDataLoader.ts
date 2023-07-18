type Planet = {
  id: string;
  name: string;
}

export type HomePageDataLoaderResponse = {
  planets: Planet[]
}

const planetIndex: Record<string, number> = {
  'mercury': 0,
  'venus': 1,
  'earth': 2,
  'mars': 3,
  'jupiter': 4,
  'saturn': 5,
  'uranus': 6,
  'neptune': 7,
}

export async function homePageDataLoader(): Promise<HomePageDataLoaderResponse> {
  const response = await fetch(`${import.meta.env.VITE_API_URL as string}/Planets`);

  if (!response.ok) throw new Error('Error fetching all planets');

  const planets = (await response.json()) as Planet[];

  return { planets: planets.sort((a, b) => planetIndex[a.id] < planetIndex[b.id] ? -1 : 1) };
}