import { LoaderFunctionArgs } from "react-router-dom";

export type Planet = {
  id: string;
  name: string;
  imageUrl: string;
  distanceToSunDisplayValue: string;
  massDisplayValue: string;
  diameterDisplayValue: string;
  additionalInfo?: string;
}

export type IndividualPlanetPageDataLoaderResponse = {
  planet: Planet
}

export async function individualPlanetPageDataLoader({ params }: LoaderFunctionArgs): Promise<IndividualPlanetPageDataLoaderResponse> {
  if (!params.planetId) throw new Error('No planet ID found');

  const response = await fetch(
    `${import.meta.env.VITE_API_URL as string}/Planets/${params.planetId}`
  );

  if (!response.ok) throw new Error(`Error fetching planet '${params.planetId}'`);

  const planet = (await response.json()) as Planet;

  return { planet }
}