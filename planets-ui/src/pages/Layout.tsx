import { Outlet } from "react-router-dom";

export function Layout() {
  return (
    <main className="max-h-[100vh] h-screen max-w-screen bg-blue-950 flex text-slate-400 text-xl font-thin">
      <div className="fixed top-1/2 left-1/2 h-[1px] w-[1px] bg-white rounded-[50%] shadow-star md:shadow-star-lg" />
      <div className="relative z-10 w-full h-full flex md:grow md:max-w-4xl md:mx-auto">
        <Outlet />
      </div>
    </main>
  );
}
